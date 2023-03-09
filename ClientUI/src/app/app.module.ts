import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NavComponent } from './nav/nav.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';
import { MemberListComponent } from './members/member-list/member-list.component';
import { MemberDetailComponent } from './members/member-detail/member-detail.component';
import { ListsComponent } from './lists/lists.component';
import { MessagesComponent } from './messages/messages.component';
import { ToastrModule } from 'ngx-toastr';
import { TestErrorsComponent } from './errors/test-errors/test-errors.component';
import { ErrorInterceptor } from './_interceptors/error.interceptor';
import { NotFoundComponent } from './errors/not-found/not-found.component';
import { ServerErrorComponent } from './errors/server-error/server-error.component';
import { MemberCardComponent } from './members/member-card/member-card.component';
import { JwtInterceptor } from './_interceptors/jwt.interceptor';
import { MemberEditComponent } from './members/member-edit/member-edit.component';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { NgxGalleryModule } from '@kolkov/ngx-gallery';
import { NgxSpinnerModule } from 'ngx-spinner';
import { LoadingInterceptor } from './_interceptors/loading.interceptor';
import { PhotoEditorComponent } from './members/photo-editor/photo-editor.component';
import { FileUploadModule } from 'ng2-file-upload';
import { SocialMediasComponent } from './social-medias/social-medias.component';
import { FacebookComponent } from './social-medias/facebook/facebook.component';
import { TextInputComponent } from './_customForms/text-input/text-input.component';
import { DateInputComponent } from './date-input/date-input.component';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import {ButtonsModule} from 'ngx-bootstrap/buttons'
import { TimeagoModule } from 'ngx-timeago';
import { MemberMessagesComponent } from './members/member-messages/member-messages.component';
import { MemberChatComponent } from './members/member-chat/member-chat.component';
import { AdminPanelComponent } from './admin/admin-panel/admin-panel.component';
import { HasRoleDirective } from './_directives/has-role.directive';
import { ManageUserComponent } from './admin/manage-user/manage-user.component';
import { ManagePhotoComponent } from './admin/manage-photo/manage-photo.component';
import { RolesModalComponent } from './modals/roles-modal/roles-modal.component';
import { ModalModule } from 'ngx-bootstrap/modal';
import { SharedModule } from './_modules/shared.module';

import { DefaultModule } from './default/default.module';



@NgModule({
  declarations: [
    AppComponent,
    // NavComponent,
    HomeComponent,
    RegisterComponent,
    MemberListComponent,
    MemberDetailComponent,
    ListsComponent,
    MessagesComponent,
    TestErrorsComponent,
    NotFoundComponent,
    ServerErrorComponent,
    MemberCardComponent,
    MemberEditComponent,
    PhotoEditorComponent,
    SocialMediasComponent,
    FacebookComponent,
    TextInputComponent,
    DateInputComponent,
    MemberMessagesComponent,
    MemberChatComponent,
    AdminPanelComponent,
    HasRoleDirective,
    ManageUserComponent,
    ManagePhotoComponent,
    RolesModalComponent,
    
  //  DashboardComponent,
    
  
  ],
  imports: [
    BrowserModule,
    AppRoutingModule, 
    HttpClientModule, 
    BrowserAnimationsModule,
    FormsModule,
    SharedModule,
   DefaultModule,
    // BsDropdownModule.forRoot(),
    // ToastrModule.forRoot({
    //   positionClass:'toast-bottom-right'
    // }), 
    ReactiveFormsModule,
    // TabsModule.forRoot(), 
    // NgxGalleryModule, 
    NgxSpinnerModule, 
    //FileUploadModule, 
    //BsDatepickerModule, 
   // PaginationModule.forRoot(),
    ButtonsModule,
    TimeagoModule.forRoot(),
    ModalModule.forRoot(),
   
  ],
  providers: [
           {provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi:true }, 
           {provide:HTTP_INTERCEPTORS, useClass:JwtInterceptor, multi:true}, 
           {provide:HTTP_INTERCEPTORS, useClass:LoadingInterceptor, multi:true}
  ],
  bootstrap: [AppComponent],
  
})
export class AppModule {}
