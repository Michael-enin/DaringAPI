import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SidebarComponent } from './components/sidebar/sidebar.component';
import { FooterComponent } from './components/footer/footer.component';
import { HeaderComponent } from './components/header/header.component';
import {MatDividerModule} from '@angular/material/divider'
import {MatToolbarModule} from '@angular/material/toolbar'
import {MatIconModule} from '@angular/material/icon'
import {MatButtonModule} from '@angular/material/button'; 
import {FlexLayoutModule} from '@angular/flex-layout'
import {MatMenuModule} from '@angular/material/menu'
import {MatListModule} from '@angular/material/list'
import { RouterModule } from '@angular/router';

import { HighchartsChartModule } from 'highcharts-angular';
import { CardComponent } from './widgets/card/card.component';
import { AreaComponent } from './widgets/area/area.component';
import { PieComponent } from './widgets/pie/pie.component';
import { PostsComponent } from './components/posts/posts.component';
import { NavComponent } from '../nav/nav.component';
import { FormsModule } from '@angular/forms';
import { MemberComponent } from './components/sidebar/member/member.component';

import { ListMemberComponent } from './components/sidebar/list-member/list-member.component';
import { UserDetailComponent } from './components/sidebar/user-detail/user-detail.component';



@NgModule({
  imports: [
    CommonModule, 
     
    MatDividerModule, 
    MatToolbarModule, 
    MatIconModule, 
    MatButtonModule, 
    FlexLayoutModule,
    MatMenuModule,
    MatListModule, 
    HighchartsChartModule,
    RouterModule,
    FormsModule
   
  ], 
  declarations: [
    // HeaderComponent,
    NavComponent,
    SidebarComponent,
    FooterComponent,
    CardComponent,
    AreaComponent,
    PieComponent,
    PostsComponent,
    MemberComponent,   
    ListMemberComponent,
    UserDetailComponent,
   
  ],
  exports:[
    // HeaderComponent, 
    NavComponent,
    SidebarComponent, 
    FooterComponent,
    CardComponent,
    AreaComponent,
    PieComponent,
    MemberComponent,
    ListMemberComponent
  ]
})
export class DefaultSharedModule { }
