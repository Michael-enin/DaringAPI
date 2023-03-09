import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { ToastrModule, ToastrService } from 'ngx-toastr';

import { NgxGalleryModule } from '@kolkov/ngx-gallery';
import { FileUploadModule } from 'ng2-file-upload';
import {BsDatepickerModule} from 'ngx-bootstrap/datepicker'
import {PaginationModule} from 'ngx-bootstrap/pagination'
import { TimeagoModule } from 'ngx-timeago';
import { TabsModule } from 'ngx-bootstrap/tabs';
import {ModalModule} from 'ngx-bootstrap/modal'
import { MatToolbarModule} from '@angular/material/toolbar'
import { MatIconModule} from '@angular/material/icon'
import { MatMenuModule} from '@angular/material/menu'
import { MatListModule} from '@angular/material/list'

@NgModule({
  declarations: [],
  imports: [
    CommonModule, 
    BsDropdownModule.forRoot(),
    ToastrModule.forRoot({
      positionClass:'toastr-bottom-right'
    }),
    TabsModule.forRoot(), 
    NgxGalleryModule, 
    FileUploadModule, 
    BsDatepickerModule.forRoot(),
    ModalModule.forRoot(),
    PaginationModule.forRoot(),
    TimeagoModule.forRoot(),
    MatToolbarModule,
    MatIconModule,
    MatMenuModule,
    MatListModule

  ],
  exports:[
    BsDropdownModule, 
    ToastrModule, 
    NgxGalleryModule, 
    FileUploadModule, 
    BsDatepickerModule, 
    ModalModule,
    TimeagoModule,
    PaginationModule,
    TabsModule, 
    MatToolbarModule,
    MatIconModule,
    MatMenuModule,
    MatListModule
   
  ]
})
export class SharedModule { }
