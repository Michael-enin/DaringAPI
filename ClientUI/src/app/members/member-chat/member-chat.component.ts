import { HttpClient } from '@angular/common/http';
import { Component, Input, OnInit } from '@angular/core';
import { MembersService } from 'src/app/_services/members.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-member-chat',
  templateUrl: './member-chat.component.html',
  styleUrls: ['./member-chat.component.css']
})
export class MemberChatComponent implements OnInit {
  baseUrl=environment.apiUrl;
@Input() senderAvator:string;
@Input() senderName:string;
  constructor(private http:HttpClient, private memberService:MembersService) { }

  ngOnInit(): void {
    

  }
 

}
