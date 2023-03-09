import { Component, HostListener, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { take } from 'rxjs/operators';
import { Member } from 'src/app/_models/member';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { MembersService } from 'src/app/_services/members.service';

@Component({
  selector: 'app-member-edit',
  templateUrl: './member-edit.component.html',
  styleUrls: ['./member-edit.component.css']
})
export class MemberEditComponent implements OnInit {
  _member!:Member;
   user!:User;
   @ViewChild('editForm') editForm!: NgForm;
   @HostListener('window:beforeunload', ['$event']) unloadNotification($event:any){
     if(this.editForm.dirty){
       $event.returnValue=true;
     }
   }
  constructor(private accountService:AccountService, 
              private memberService:MembersService, 
              private toastr:ToastrService) { 
                this.accountService.currentUser$.pipe(take(1)).subscribe(userData=>{
                this.user=userData;
                console.log("user "+ this.user);
                })
             // 
              }
  ngOnInit(): void {
    this.loadMember();
  }
  loadMember(){
    this.memberService.getUser(this.user.userName).subscribe(I_am_member=>
      this._member=I_am_member
      )
     console.log(this._member);
  }
  updateProfile(){
    this.memberService.updateMember(this._member).subscribe(()=>{
      this.toastr.success("Your Profile is Update!");
      this.editForm.reset(this._member);
    console.log(this._member);
    })
   
    
  }

}
