import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ReplaySubject } from 'rxjs';
import {map} from 'rxjs/operators'
import { environment } from 'src/environments/environment';
import { User } from '../_models/user';
import { AvailableUserService } from './available-user.service';
@Injectable({
  //instead of injecting the service in the following manner 
  // you can place in module providers array
  providedIn: 'root'
})
export class AccountService {
  //to avoid hard code, put the baseUrl in environment.ts file
  baseUrl=environment.apiUrl;
  private currentUserSource = new ReplaySubject<User>(1);
  currentUser$ = this.currentUserSource.asObservable();
  meAsCurrentUser:any;
  loggedInUser:string;
  constructor(private http:HttpClient, private availableServ:AvailableUserService) {
    
   }
   //model is response data
   login(model: any){
     //pipe is from Rxjs
     return this.http.post<User>(this.baseUrl+'/account/login', model)
                        .pipe(map((responseData : User) => {
                                      const user = responseData;
                                      if(user){
                                        this.setCurrentUser(user);
                                        this.availableServ.createHubConnection(user);
                                      //   localStorage.setItem('user', JSON.stringify(user));
                                      //  // this.meAsCurrentUser=JSON.parse(localStorage.getItem('user') || '{}');
                                      //   this.currentUserSource.next(user);
                                      /* very important Account need to be exported*/
                                        this.loggedInUser=responseData.userName;
                                      // console.log(responseData.userName);
                                      }
                                      
                                  })      
                                );
   }
  register(model:any){
    return this.http.post<User>(this.baseUrl+'/account/register', model).pipe(
      // the object we are receiving is not specified use 'any' instead
      map((user : User) =>{
        if(user){
          this.setCurrentUser(user);
          this.availableServ.createHubConnection(user);
         // localStorage.setItem('user', JSON.stringify(user));
        //  this.currentUserSource.next(user);
        }
        return user;
      }
    ));
  }
   setCurrentUser(user:User){  
     user.roles = []; 
     //the data or 'role' decoded from the array may or maynot be an array
     /*
         "role":[
           "Admin",
           "Moderator"
         ] array
         Or 
         "role":"Member" not array
     
     */
      const mRoles = this.getDecodeTokenInfo(user.token).role;
      Array.isArray(mRoles) ? user.roles = mRoles : user.roles.push(mRoles);
      localStorage.setItem('user', JSON.stringify(user)); 
      this.currentUserSource.next(user);        
   }
   logout(){
    localStorage.removeItem('user');
      const user: any=null;
      this.currentUserSource.next(null);
 //   localStorage.clear();
    this.availableServ.stopHubConnection();
   }
   getDecodeTokenInfo(token){
     return JSON.parse(atob(token.split('.')[1]));
   }
}
