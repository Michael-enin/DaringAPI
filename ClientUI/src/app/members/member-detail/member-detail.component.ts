import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NgxGalleryAnimation, NgxGalleryImage, NgxGalleryOptions, NgxGalleryThumbnailsComponent } from '@kolkov/ngx-gallery';
import { TabDirective, TabsetComponent } from 'ngx-bootstrap/tabs';
import { take } from 'rxjs/operators';
import { Member } from 'src/app/_models/member';
import { DummyMessage } from 'src/app/_models/message';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { AvailableUserService } from 'src/app/_services/available-user.service';
import { MembersService } from 'src/app/_services/members.service';
import { MessageService } from 'src/app/_services/message.service';



@Component({
  selector: 'app-member-detail',
  templateUrl: './member-detail.component.html',
  styleUrls: ['./member-detail.component.css']
})
export class MemberDetailComponent implements OnInit, OnDestroy {
  @ViewChild('memberTabs', {static: true}) memberTabs:TabsetComponent;
  _member!:Member;
  galleryOptions!: NgxGalleryOptions[];
  galleryImages!: NgxGalleryImage[];

  activeTab:TabDirective;
  _messages: DummyMessage[] = [];
  user:User;

  constructor(
              private route:ActivatedRoute, 
              private messageService:MessageService, 
              public availableUserService : AvailableUserService, 
              public accountService:AccountService)
               { 
                 this.accountService.currentUser$.pipe(take(1)).subscribe(user =>this.user=user)              
               }


  ngOnInit(): void {
    // this.loadMemberUser();
    this.route.data.subscribe(respData=>{
              this._member = respData.member;            
            })
    this.route.queryParams.subscribe(params=>{
         params.tab ? this.selectMessageTab(params.tab):this.selectMessageTab(0)
    })
    this.galleryOptions = [
      {
        width: '600px',
        height: '600px',
        thumbnailsColumns: 4,
        imageAnimation: NgxGalleryAnimation.Slide, 
        preview:false
      }
    ]  
    this.galleryImages=this.loadUserImages();
  }
loadUserImages():NgxGalleryImage[]{
  const imgUrls:any=[];
  for(const photo of this._member.photos){
    imgUrls.push({
      small:photo?.url,
      medium:photo?.url,
      big:photo?.url
    })
     
  }
  return imgUrls;
}
  // loadMemberUser(){
  //              this.memberService.getUser(this.route.snapshot.paramMap.get('username') || '{}')
  //                              .subscribe(loadedMember=>{
  //                               this._member=loadedMember;
  //                               // this.galleryImages=this.loadUserImages();
  //                             });
                               
                                             
  //            }
loadMessages(){
  this.messageService.getMessageThread(this._member.knownAs).subscribe(response=>{
    this._messages = response;

    console.log("your Messages are "+response.values);
  })

}
selectMessageTab(tabId:number){
  this.memberTabs.tabs[tabId].active = true;
}
            
onTabActivated(data:TabDirective){
      this.activeTab = data;
      if(this.activeTab.heading==='Messages' && this._messages.length === 0){
        //messages must be loaded only when you activated 'Messages' tab
       // this.loadMessages(); no need of this method, cause we get message from MessageHub
       this.messageService.createHubConnection(this.user, this._member.knownAs.toLowerCase());
      }
      else{
       this.messageService.stopHubConnection();
      }
  }
  ngOnDestroy(): void {
     this.messageService.stopHubConnection();
  }
}
