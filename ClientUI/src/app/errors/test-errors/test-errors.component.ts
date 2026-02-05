import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-test-errors',
  templateUrl: './test-errors.component.html',
  styleUrls: ['./test-errors.component.css']
})
export class TestErrorsComponent implements OnInit {

   mainUrl='https://localhost:5001/api';
   validationErrors:string[]=[];
  constructor(private http: HttpClient) { }

  ngOnInit(): void {
  }
  get404Error(){
      return this.http.get(this.mainUrl+'/erraneous/not-found').subscribe(
        ResponseData=>{
          console.log(ResponseData);
        }, 
        error=>{console.log(error);
       }, 
       ()=>{
         console.log("Errors Catch Done!");

       }
      )
  }
  get400Error(){
    return this.http.get(this.mainUrl+'/erraneous/bad-request').subscribe(
      ResponseData=>{
        console.log(ResponseData);
      }, 
      error=>{console.log(error);
     }, 
     ()=>{
       console.log("Errors Catch Done!");

     }
    )
  }
  get500Error(){
    return this.http.get(this.mainUrl+'/erraneous/server-error').subscribe(
      ResponseData=>{
        console.log(ResponseData);
      }, 
      error=>{console.log(error);
     }, 
     ()=>{
       console.log("Errors Catch Done!");

     }
    )
  }
  get401Error(){
    return this.http.get(this.mainUrl+'/erraneous/auth').subscribe(
      ResponseData=>{
        console.log(ResponseData);
      }, 
      error=>{console.log(error);
     }, 
     ()=>{
       console.log("Errors Catch Done!");

     }
    )
  }
  get400ValidationError(){
    return this.http.post(this.mainUrl+'/account/register', {}).subscribe(
      ResponseData=>{
        console.log(ResponseData);
      }, 
      error=>{console.log(error);
        this.validationErrors=error;
     }, 
     ()=>{
       console.log("Errors Catch Done!");

     }
    )

  }

}
