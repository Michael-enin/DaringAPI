import { Component, OnInit } from '@angular/core';


import { Observable } from 'rxjs';
import { take } from 'rxjs/operators';

import { Member } from 'src/app/_models/member';
import { Pagination } from 'src/app/_models/Pagination';
import { User } from 'src/app/_models/user';
import { UserInputParams } from 'src/app/_models/userInputParams';
import { AccountService } from 'src/app/_services/account.service';
import { MembersService } from 'src/app/_services/members.service';

@Component({
  selector: 'app-member-list',
  templateUrl: './member-list.component.html',
  styleUrls: ['./member-list.component.css']
})
export class MemberListComponent implements OnInit {
  // memberes!:Member[];
  //memberes$!:Observable<Member[]>;
 members!:Member[];
 pagination:Pagination;
//  pageNumber=1;
//  PageSize=5;
userInputParams:UserInputParams;
user:User;
genderList = [{value:'male', display:'Males'}, {value:'female', display:'Females'}]
  
  // constructor(private memberService:MembersService, private accountService:AccountService) {
    constructor(private memberService:MembersService) {
    // this.accountService.currentUser$.pipe(take(1)).subscribe(user=>
    //   {
    //       this.user = user;
    //       this.userInputParams=new UserInputParams(user);
    // })
this.userInputParams=this.memberService.getUserInputParams();
   }

  ngOnInit(): void {
   //this.memberes$ = this.memberService.getUsers();
   this.loadMembers();
   //console.log("nothing displayed ");
  }
  // loadMembers(){
  //   this.memberService.getUsers().subscribe(usersData=>{
  //   this.memberes=usersData;
  //   })
  // }
  loadMembers(){
    // this.memberService.getMembers(userInputParams, this.PageSize).subscribe(
            this.memberService.setUserInputParams(this.userInputParams);
            this.memberService.getMembers(this.userInputParams).subscribe(response=>{     
              this.members=response.result;
              this.pagination=response.pagination;
              console.log("number of users "+this.members.length);
      }
    )
  }
  onclick(){
  
  }
  resetFilter(){
    this.userInputParams = this.memberService.resetUserParams();
    this.loadMembers();
  }
  pageChangedMethod(event:any){
    this.userInputParams.pageNumber=event.page;
    this.memberService.setUserInputParams(this.userInputParams);
    this.loadMembers();

  }

}
