import { Component, Input, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Member } from 'src/app/_models/member';
import { Pagination } from 'src/app/_models/Pagination';
import { User } from 'src/app/_models/user';
import { UserInputParams } from 'src/app/_models/userInputParams';
import { AccountService } from 'src/app/_services/account.service';
import { AvailableUserService } from 'src/app/_services/available-user.service';
import { MembersService } from 'src/app/_services/members.service';

@Component({
  selector: 'app-member',
  templateUrl: './member.component.html',
  styleUrls: ['./member.component.css']
})
export class MemberComponent implements OnInit {
  @Input() member!:Member;
 // members!:Member[];
  pagination:Pagination;
  userInputParams:UserInputParams;
  user:User;

  customPhotoUrl = 'assets/images/dummyImages/me.jpg';
  usersOnline:string[];
  constructor( private memberService:MembersService, 
               private availableUserService:AvailableUserService, 
               private accountService:AccountService
               ) { }
  ngOnInit(): void {
    this.availableUserService.onlineUsers$.subscribe(user=>{
      this.usersOnline.push(user.toString());
      console.log("Is Displayed On Sider Bar?")
    })
   // this.loadMembers();
  }
}
