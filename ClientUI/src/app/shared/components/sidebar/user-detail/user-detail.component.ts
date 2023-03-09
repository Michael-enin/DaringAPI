import { Component, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { NgxGalleryAction, NgxGalleryImage, NgxGalleryOptions } from '@kolkov/ngx-gallery';
import { TabDirective, TabsetComponent } from 'ngx-bootstrap/tabs';
import { Member } from 'src/app/_models/member';
import { DummyMessage } from 'src/app/_models/message';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { AvailableUserService } from 'src/app/_services/available-user.service';
import { MembersService } from 'src/app/_services/members.service';

@Component({
  selector: 'app-user-detail',
  templateUrl: './user-detail.component.html',
  styleUrls: ['./user-detail.component.css']
})
export class UserDetailComponent implements OnInit {
  // @ViewChild('memberTabs', {static: true}) memberTabs:TabsetComponent;
  // _member!:Member;
  //  galleryOptios:NgxGalleryOptions[]; //gallery options 
  //  galleryImages!:NgxGalleryImage[]; //images List from gllery
  //  activeTab:TabDirective;
  //  user!:User;
  //  messages:DummyMessage[];
usersOnline:string[];
  constructor( private membersService:MembersService, 
               private availableUserService:AvailableUserService, 
               private accountService:AccountService
               ) { }

  ngOnInit(): void {
    this.availableUserService.onlineUsers$.subscribe(user=>{
      this.usersOnline.push(user.toString());
    })
  }
  loadAllUsers(){
    
  }
  

}
