import { Component, Input, OnInit } from '@angular/core';
import { Member } from 'src/app/_models/member';
import { UserInputParams } from 'src/app/_models/userInputParams';

import { AccountService } from 'src/app/_services/account.service';
import { MembersService } from 'src/app/_services/members.service';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.scss']
})
export class SidebarComponent implements OnInit {
  @Input() userNameFromParent:string;
  members: Member[]=[];
 // userInputParams:UserInputParams;
constructor(public accountService:AccountService, 
              private memberService:MembersService) { 
        //  this.userInputParams=this.memberService.getUserInputParams();
              }

  ngOnInit(): void {
  console.log("Is Displayed SideBar");
  this.loadAllMembers();
  
  }
 
  loadAllMembers(){
    this.memberService.getAllMembers().subscribe(data=>{
      this.members=data;  
      console.log(" loaded members "+ this.members.length)
    });
    
   
  }
  
 }

