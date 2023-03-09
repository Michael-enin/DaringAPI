import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { map, take } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { MemberEditComponent } from '../members/member-edit/member-edit.component';
import { Member } from '../_models/member';
import { PaginatedResult } from '../_models/Pagination';
import { User } from '../_models/user';
import { UserInputParams } from '../_models/userInputParams';
import { AccountService } from './account.service';
import { getPaginatedResult, getPaginationHeader } from './paginationHelper';

//this is like an interceptor
// we created jwt interceptor instead of the following one
// this is no longer needed any more
             /*.........<<<>>>..............*/
// const httpOPtions = {
//   headers:new HttpHeaders({
//       Authorization: 'Bearer ' + JSON.parse(localStorage.getItem('user') || '{}').token
//     })}



@Injectable({
  providedIn: 'root'
})

export class MembersService {

  baseUrl=environment.apiUrl;
  users:any;
  members:Member[]=[];
  allMembers :Member[]=[];
  memberStorage = new Map();
  user:User;
  userInputParams:UserInputParams

  constructor(private http:HttpClient, 
              private accountService:AccountService)
       {
              this.accountService.currentUser$.pipe(take(1)).subscribe(user=>
                  {
                      this.user = user;
                      this.userInputParams=new UserInputParams(user);
                })
    
   }
   getUserInputParams(){
     return this.userInputParams;
   }
   setUserInputParams(params:UserInputParams){
     this.userInputParams=params;
   }
   resetUserParams(){
     this.userInputParams = new UserInputParams(this.user);
     return this.userInputParams;
   }
   //this needs to be commented!

   getUsers(){
     if(this.members.length>0){
       return of(this.members);
     }
    return this.http.get<Member[]>(this.baseUrl+'/users').pipe(
      map(memberResponseData=>{
        this.members=memberResponseData;
        return memberResponseData;
      })
    )
   }
   //comment end
  //  getMembers(page?:number, itemsPerPage?:number){
  //     let params = new HttpParams();
  //     if(params!==null && itemsPerPage!==null){
  //       // params = params.append("pageNumber",page.toString());
  //       // params = params.append("pageSize", itemsPerPage.toString())
  //     }
  getMembers(userParams: UserInputParams ){
    // console.log(Object.values(userParams).join("_"));
    // Object.values("something") is to get keys
    //object.keys("something") is to get values

    var responseData = this.memberStorage.get(Object.values(userParams).join('_'));
    if(responseData){
      return of(responseData);
    }
  //  console.log(responseData);
      // let params = this.getPaginationHeader(userParams.pageNumber, userParams.pageSize);
      let params = getPaginationHeader(userParams.pageNumber, userParams.pageSize);
      params = params.append('mingAge', userParams.minAge.toString());
      params = params.append('maxAge', userParams.maxAge.toString());
      params = params.append('gender', userParams.gender);
      params = params.append('orderBy', userParams.orderBy);

      // return this.getPaginatedResult<Member[]>(this.baseUrl+'/users', params)
      return getPaginatedResult<Member[]>(this.baseUrl+'/users', params, this.http)
      .pipe(map(mResponse=>{
        // var key = Object.values(userParams).join("_");
        // this.memberStorage.set(key, mResponse);
        this.memberStorage.set(Object.values(userParams).join("_"), mResponse);
        return mResponse;
      }))
   }
  // private getPaginatedResult<T>(url, params) {
  //  const paginatedResult : PaginatedResult<T> = new PaginatedResult<T>();
  //   return this.http.get<T>(url, { observe: 'response', params })
  //     .pipe(
  //       map(response => {
  //         paginatedResult.result = response.body;
  //         if (response.headers.get("Pagination") !== null) {
  //           paginatedResult.pagination = JSON.parse(response.headers.get("Pagination"));
  //         }
  //         return paginatedResult;
  //       })
  //     );
  // }

  //  private getPaginationHeader(pageNumber:number, pageSize:number){
  //    let params = new HttpParams();
  //         params = params.append("pageNumber",pageNumber.toString());
  //         params = params.append("pageSize", pageSize.toString())
  //         return params;
  //  }
   getUser(username:string){
    //  const member = this.members.find(x=>x.username===username);
    //  if(member!==undefined){
    //     return of(member);
    //  }
    //console.log(this.memberStorage);
    const member = [...this.memberStorage.values()]
                       .reduce((arr, elem)=>arr.concat(elem.result), [])
                       .find((member:Member)=>member.username===username);
                                          
    if(member){
      return of(member);
    }
  // console.log("member not Found");
     return this.http.get<Member>(this.baseUrl+'/users/'+username)
   }
   updateMember(member:Member){
      return this.http.put(this.baseUrl+'/users', member).pipe(
        map(()=>{
          const index = this.members.indexOf(member);
          this.members[index]=member;
           })
      );
   }
   setProfilePhoto(photoId:number){
      return this.http.put(this.baseUrl+'/users/set-profile-photo/'+photoId, {})
   }
   deletePhoto(photoId:number){
     return this.http.delete(this.baseUrl+'/users/delete-photo/'+photoId);
   }
   addLike(username:string){
    return this.http.post(this.baseUrl+'/likes/'+ username, {})
   }
   getLikes(predicate:string, pageSize, pageNumber){
     // let params = this.getPaginationHeader(pageSize, pageNumber);
     let params = getPaginationHeader(pageSize, pageNumber);
     params = params.append('predicate', predicate);
    //return this.http.get<Partial<Member[]>>(this.baseUrl+'/likes?predicate='+ predicate);
   // return this.getPaginatedResult<Partial<Member[]>>(this.baseUrl+'/likes', params); 
    return getPaginatedResult<Partial<Member[]>>(this.baseUrl+'/likes', params, this.http); 
  }
  //  getLikes(predicate:string){
  //   return this.http.get<Partial<Member[]>>(this.baseUrl+'/likes?predicate='+ predicate);
  //  }
  getAllMembers(){
    return this.http.get<Member[]>(this.baseUrl+'/custommembers/').pipe(
      map(data =>{
        this.allMembers =data
        return data;
      })
    )
  }
}
