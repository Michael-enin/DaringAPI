import { Component, Input, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Member } from 'src/app/_models/member';
import { AccountService } from 'src/app/_services/account.service';
import { AvailableUserService } from 'src/app/_services/available-user.service';
import { MembersService } from 'src/app/_services/members.service';

@Component({
  selector: 'app-member-card',
  templateUrl: './member-card.component.html',
  styleUrls: ['./member-card.component.css']
})
export class MemberCardComponent implements OnInit {
  myName:string;
  usersOnline:string[]=[]
@Input() member!:Member;
  constructor(private memberService:MembersService, 
               private toastr:ToastrService, 
               public availableUserService:AvailableUserService, 
               public accountService:AccountService) { }
  ngOnInit(): void {
    console .log( "active users are "+ this.availableUserService.activeUsersList)
   this.accountService.currentUser$.subscribe(user =>{
          this.myName = user.knownAs;
         
   })
    console.log("The current user is " +this.myName + '\n Member User: '+this.member.knownAs.toLowerCase());
   // this.usersOnline.push(this.myName);
            this.availableUserService.onlineUsers$.subscribe(usr =>{
            this.usersOnline.push(usr.toString());
    }) 
   // console.log("All Users "+ this.usersOnline)  
  }
  addLike(memb:Member){
    this.memberService.addLike(memb.knownAs).subscribe(()=>{
   // this.toastr.success('You liked' +memb.knownAs);
      console.log('You liked '+ memb.knownAs );
      this.toastr.success("You are verly liked "+memb.knownAs);
   })
 // console.log("no user Found");
   }


}
