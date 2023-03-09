import { Injectable } from '@angular/core';
import { async } from '@angular/core/testing';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { ToastrService } from 'ngx-toastr';
import { BehaviorSubject, concat } from 'rxjs';
import { environment } from 'src/environments/environment';
import { User } from '../_models/user';

@Injectable({
  providedIn: 'root'
})
export class AvailableUserService {
  hubUrl = environment.hubUrl;
  private hubConnection:HubConnection;
  private onlineUsersSource = new BehaviorSubject<string[]>([]);
  onlineUsers$ = this.onlineUsersSource.asObservable(); 
   activeUsersList:any[]=[];
  

  constructor(private toastr:ToastrService) { }
  createHubConnection(user:User){
    this.hubConnection = new HubConnectionBuilder()
      .withUrl(this.hubUrl + 'available', {
           accessTokenFactory:()=>user.token})
        .withAutomaticReconnect()
        .build()
        this.hubConnection.start().catch(error =>console.log( "Connection Problem:  "+error));
        this.hubConnection.on('UserIsOnline', username=>{
          this.toastr.success(username.toUpperCase() + ' is online');
          this.activeUsersList.push(username);
        });
        this.hubConnection.on('UserIsOffline', username=>{
          this.toastr.warning(username.toUpperCase() + ' is disconneted');
          this.removeElement(username);
        })
        this.hubConnection.on("GetOnlineUsers", (usernames:string[])=>{
          this.onlineUsersSource.next(usernames);
        })
        console.log("Who are online Users? "+this.onlineUsers$.subscribe(r=>console.log(r.values)));
    }
    stopHubConnection(){
      this.hubConnection.stop().catch(error=>console.log(error));
    }
    removeElement(eleme){
      this.activeUsersList.forEach((value, index)=>{
        if(value==eleme)
        this.activeUsersList.splice(index, 1);
      })
    }
}
