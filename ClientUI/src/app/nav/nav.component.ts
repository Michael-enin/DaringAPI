import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { User } from '../_models/user';

import { AccountService } from '../_services/account.service';


@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
model:any={}
loggedIn!:boolean;
@Output() toggleSideBarEvent : EventEmitter<any> = new EventEmitter();
//currentUser!:Observable<User>;
  constructor(public accountService:AccountService, private route : Router, 
    private toastr : ToastrService) { }

  ngOnInit(): void {
    ///this.getCurrentUser();
   // this.currentUser=this.accountService.currentUser;
  }
  toggleSideBar(){
    this.toggleSideBarEvent.emit();
    setTimeout(()=>{
      window.dispatchEvent(
        new Event('resize'));
    }, 300);
  }
  login(){
   // console.log(this.model);
    this.accountService.login(this.model).subscribe(responseData=>{
      this.route.navigateByUrl('/members');
      this.toastr.error(" You are Kidnapped, no Pass!" + responseData)
    
      
    //  console.log(responseData);
    //  this.loggedIn=true;
    
    }, error=>{
     // console.log(error);
  this.toastr.error(error.error);
    });
    
  }
  logout(){
    this.accountService.logout();
    this.route.navigateByUrl('/');
  // this.loggedIn=false;
  }
  // getCurrentUser(){
  //   this.accountService.currentUser.subscribe(user=>{
  //     this.loggedIn=!!user;
  //   }, error=>{
  //     console.log(error);
  //   }, ()=>{
  //     console.log("logout success");
  //   }
  //   )
  // }
  

}
