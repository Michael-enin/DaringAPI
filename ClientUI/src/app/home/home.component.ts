import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
   usersPath:string='https://localhost:5001/api/users';
   registerMode=false;
   //users:any;
  //constructor(private http:HttpClient) { }
  constructor(private accoutService:AccountService) { }
  ngOnInit(): void {
   // this.getUsers();
  }
  registerToggle(){
    this.registerMode=!this.registerMode;
  }
  //get users from Api, that then gets users from Users table 
  // getUsers(){
  //   this.http.get('https://localhost:5001/api/users').subscribe(allUsers=>{
  //     // now all users 'allUsers' from the usersPath are
  //     // loaded to local 'users' because of subscription
  //     // no need of curly braces as every thing is in single line 
  //            this.users=allUsers
  // });
  //    }
     cancelRegisterMode(event: boolean){
       this.registerMode=event;
     }
  }
