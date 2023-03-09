import { Component, OnInit } from '@angular/core';
import {HttpClient} from '@angular/common/http'
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { User } from './_models/user';
import { AccountService } from './_services/account.service';
import { AvailableBsVersions } from 'ngx-bootstrap/utils';
import { AvailableUserService } from './_services/available-user.service';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  users:any
  title = 'This is Very Daring Application';
  constructor(private http: HttpClient,
              private accountService: AccountService, 
              private availaUserServ:AvailableUserService){

  }
  ngOnInit(){
  //  this.getAllUsers();
  this.setCurrentUser();
  }
  setCurrentUser(){
    const user : User = JSON.parse(localStorage.getItem('user'));
    if (user) {
      this.accountService.setCurrentUser(user);
      this.availaUserServ.createHubConnection(user);
    }
  }
  // getAllUsers(){
  //   this.http.get('https://localhost:5001/api/users').subscribe(responseData=>{
  //     this.users=responseData;
  //     console.log("This are loaded Users:- "+ this.users.knownAs);
  //   }, error=>{
  //     console.log(error); 
  //   }
  //   )
  // }
}
