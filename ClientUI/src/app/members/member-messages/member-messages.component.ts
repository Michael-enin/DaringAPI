import { Component, 
         Input, 
         OnInit, 
         ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { DummyMessage } from 'src/app/_models/message';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { MessageService } from 'src/app/_services/message.service';

@Component({
  selector: 'app-member-messages',
  templateUrl: './member-messages.component.html',
  styleUrls: ['./member-messages.component.css']
})
export class MemberMessagesComponent implements OnInit {
  // @Input() uName:string;
  // messages:DummyMessage[];
  // senderPhoto:DummyMessage;
  // @Input() mSenderPhoto:string;

  @ViewChild('messageForm') messageForm: NgForm;
  @Input() messages: DummyMessage[];
  @Input() userName: string;
  @Input() sndrUser:User;
  messageContent:string;
  senderMessage=false;
  currentSenderName:string;

  constructor(public messageService:MessageService, 
             public accountService:AccountService) { }
 

  ngOnInit(): void {
  this.loadMessages();
   
  }
  loadMessages(){
     this.messageService.getMessageThread(this.userName).subscribe(response=>{
      this.messages = response;
      
    })  
  }
  
  sendMessage(){
    // this.messageService.sendMessage(this.userName, this.messageContent).subscribe(responseMessages => {    
      // this following is promise not observable, therefor there no subscribe property in promise, 
      // use 'then' property for promise  
        this.messageService.sendMessage(this.userName, this.messageContent).then(()=> {          
          // this.messages.push(responseMessages);
          // this.senderMessage=true;
        
          //       //clear the form after message sent
          //       this.currentSenderName=this.accountService.loggedInUser;
          //       console.log("current Logged In User:- "+this.accountService.loggedInUser);
                this.messageForm.reset();               
              });
  }

}
