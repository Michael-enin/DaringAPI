import { Component, EventEmitter, Input, OnInit } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { User } from 'src/app/_models/user';


@Component({
  selector: 'app-roles-modal',
  templateUrl: './roles-modal.component.html',
  styleUrls: ['./roles-modal.component.css']
})
export class RolesModalComponent implements OnInit {
  @Input() updateCurrentRole = new EventEmitter();
  user:User;
  roles:any[];

  
  closeBtnName:string;

  constructor(public modalRef:BsModalRef) { }

  ngOnInit(): void {
  }
  updateRole(){
    this.updateCurrentRole.emit(this.roles);
    this.modalRef.hide();
  }

}
