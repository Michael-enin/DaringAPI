import { HttpClient } from '@angular/common/http';

import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { BehaviorSubject } from 'rxjs';
import { take } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { DummyMessage } from '../_models/message';
import { PaginatedResult } from '../_models/Pagination';
import { User } from '../_models/user';
import { getPaginatedResult, getPaginationHeader } from './paginationHelper';

@Injectable({
  providedIn: 'root'
})
export class MessageService {
  baseUrl = environment.apiUrl;
  hubUrl = environment.hubUrl;
  private hubConnection : HubConnection;
  private messageThreadStorage = new BehaviorSubject<DummyMessage[]>([]);
  messageThread$ = this.messageThreadStorage.asObservable();

  constructor(private http:HttpClient) { }
  createHubConnection(user:User, otherUser:string){
    this.hubConnection  = new HubConnectionBuilder()
          .withUrl(this.hubUrl + 'messages?user=' + otherUser, 
          {
               accessTokenFactory:()=>user.token
          }).withAutomaticReconnect()
          .build()
         this.hubConnection.start()
             .catch(error =>console.log( "Connection Error " +error));
         this.hubConnection.on('ReceiveMessageThread', messages =>
                {
                    this.messageThreadStorage.next(messages)
                })
        this.hubConnection.on('NewMessage', message =>{
          this,this.messageThread$.pipe(take(1)).subscribe(messages =>{
            this.messageThreadStorage.next([...messages, message]);
          })

        })
  }
stopHubConnection(){
  if(this.hubConnection)
   this.hubConnection.stop()
}
getMessages(pageNumber, pageSize, container){
    let params = getPaginationHeader(pageNumber, pageSize);
    params = params.append('container', container);
    return getPaginatedResult<DummyMessage[]>(this.baseUrl + '/messages', params, this.http);

  }
getMessageThread(uName: string){
   return this.http.get<DummyMessage[]>(this.baseUrl+'/messages/thread/'+ uName);

  }
  async sendMessage(username:string, content:string){
  // return this.http.post<DummyMessage>(this.baseUrl+'/messages', {recipientUserName:username, content})
           /*
                ||    this return promise not observable,cause it is not http request 
                \/    and therefor no interceptor to handle errors, hence catch the error
          */      
  // return this.hubConnection.invoke('SendMessage', {reciepentUserName:username, content}); 
  return this.hubConnection.invoke('SendMessage', {recipientUserName:username, content})
                  .catch(error =>{
                    console.log(error);
                  })
}
deleteMessage(id:number){
  return this.http.delete(this.baseUrl+'/messages/'+id);
}
}
