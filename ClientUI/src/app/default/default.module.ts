import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { FlexLayoutModule } from "@angular/flex-layout";
import { MatDividerModule } from "@angular/material/divider";
import { RouterModule } from "@angular/router";
import { DashboardComponent } from "../admin/dashboard/dashboard.component";
import { DashboardService } from "../admin/dashboard/service/dashboard.service";
import { DefaultComponent } from "./default.component";
import { MatSidenavModule } from '@angular/material/sidenav'
import { MatTableModule} from "@angular/material/table"
import { MatPaginatorModule } from "@angular/material/paginator";
import { DefaultSharedModule } from "../shared/default.shared.module";

@NgModule({
  
    imports: [
      CommonModule, 
      RouterModule,
      DefaultSharedModule, 
      MatSidenavModule,
      MatDividerModule, 
      FlexLayoutModule,
      MatTableModule,
      MatPaginatorModule
    ],
    declarations: [
      DefaultComponent, 
      DashboardComponent,
      
    ],
  exports:[
    DefaultComponent
  ],

    providers:[
      DashboardService
    ]
  })
  export class DefaultModule { }