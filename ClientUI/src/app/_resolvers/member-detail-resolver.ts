import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from "@angular/router";
import { Observable } from "rxjs";
import { Member } from "../_models/member";
import { MembersService } from "../_services/members.service";

@Injectable({
    providedIn: 'root'
})
export class MemberDetailResolver implements Resolve<Member>{
    constructor(private memberservice:MembersService){
        
    }
    resolve(route: ActivatedRouteSnapshot): Observable<Member>{
        return this.memberservice.getUser(route.paramMap.get('username'));

    }

} 