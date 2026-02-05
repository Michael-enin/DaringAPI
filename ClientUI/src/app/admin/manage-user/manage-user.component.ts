import { Component, OnInit } from '@angular/core';
import { BsModalRef, BsModalService, ModalOptions } from 'ngx-bootstrap/modal';
import { RolesModalComponent } from 'src/app/modals/roles-modal/roles-modal.component';
import { User } from 'src/app/_models/user';
import { AdminService } from 'src/app/_services/admin.service';

@Component({
  selector: 'app-manage-user',
  templateUrl: './manage-user.component.html',
  styleUrls: ['./manage-user.component.css']
})
export class ManageUserComponent implements OnInit {
users : Partial<User[]>
modalRef:BsModalRef
  constructor(private adminService:AdminService, 
              private modalServ:BsModalService) { }

  ngOnInit(): void {
    this.getUsersWithRoles();
  }
  getUsersWithRoles(){
       this.adminService.getUsersWithRoles().subscribe(usersResp=>{
         this.users = usersResp;
       })
  }
  openModalOfRoles(user:User){
    const config = {
      class:'modal-dialog-centered',
      initialState : {
       user, 
       roles:this.getRolesList(user)
      
    }
  };
    this.modalRef = this.modalServ.show(RolesModalComponent, config );
    this.modalRef.content.updateCurrentRole.subscribe(values=>{
      const updatingRoles = {
        roles: [...values.filter(item=>item.checked === true).map(item=>item.name)]
      };
      if(updatingRoles){
        this.adminService.updateRole(user.userName, updatingRoles.roles).subscribe(()=>{
          user.roles = [...updatingRoles.roles];
        })
      }

    })
  
   //this.modalRef = this.modalServ.show(RolesModalComponent);
  }
  private getRolesList(user:User){
    const roles =[];
    const userRoles = user.roles;
    const existingRoles:any[]=[
                       {name:'Admin', value:'Admin'},
                       {name:'Moderator', value:'Moderator'}, 
                       {name:'Member', value:'Member'}];
    existingRoles.forEach(role=>{
      let hasMatch=false;
      for(const userRole of userRoles){
        if(role.name === userRole){
          hasMatch = true;
          role.checked=true;
          roles.push(role);
          break;
        }
      }
      if(!hasMatch){
        role.checked=false;
        roles.push(role);
      }
    })
    return roles;
  }

}
