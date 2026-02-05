import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NotFoundComponent } from './errors/not-found/not-found.component';
import { ServerErrorComponent } from './errors/server-error/server-error.component';
import { TestErrorsComponent } from './errors/test-errors/test-errors.component';
import { HomeComponent } from './home/home.component';
import { ListsComponent } from './lists/lists.component';
import { MemberDetailComponent } from './members/member-detail/member-detail.component';
import { MemberEditComponent } from './members/member-edit/member-edit.component';
import { MemberListComponent } from './members/member-list/member-list.component';
import { MessagesComponent } from './messages/messages.component';
import { AuthGuard } from './_gaurds/auth.guard';
import { PreventUnsaveChangesGuard } from './_gaurds/prevent-unsave-changes.guard';
import { MemberDetailResolver } from './_resolvers/member-detail-resolver';
import { AdminPanelComponent } from './admin/admin-panel/admin-panel.component';
import { AdminGuard } from './_gaurds/admin.guard';
import { DefaultModule } from './default/default.module';
import { DefaultComponent } from './default/default.component';
import { DashboardComponent } from './admin/dashboard/dashboard.component';
import { PostsComponent } from './shared/components/posts/posts.component';
import { ListMemberComponent } from './shared/components/sidebar/list-member/list-member.component';

const routes: Routes = [
  {path:'', component:HomeComponent}, 
  {
    path:'', runGuardsAndResolvers: 'always', 
    canActivate:[AuthGuard], 
    children:[
      
      {path:'members', component:MemberListComponent, canActivate:[AuthGuard]}, 
      {path:'members/:username', component:MemberDetailComponent, resolve:{member:MemberDetailResolver}}, 
      {path:'member/edit', component:MemberEditComponent, canDeactivate:[PreventUnsaveChangesGuard]},
      {path:'list', component:ListsComponent}, 
      {path:'messages', component:MessagesComponent},
      {path:'members', component:ListMemberComponent },
      {path:'admin', component:DefaultComponent, canActivate:[AdminGuard], 
         children:[
          {
            path:'', 
            component:DashboardComponent
          }, 
          {
            path:'posts',  
            component:PostsComponent
          }
         ]
    }
    ]
  },
   {path:'error', component:TestErrorsComponent},
   {path:'not-found', component:NotFoundComponent},
   {path:'server-error', component:ServerErrorComponent},
   {path:'**', component:NotFoundComponent, pathMatch:'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
