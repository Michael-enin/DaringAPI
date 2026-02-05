import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-server-error',
  templateUrl: './server-error.component.html',
  styleUrls: ['./server-error.component.css']
})
export class ServerErrorComponent implements OnInit {
 error:any;

  constructor(private router:Router) {
    const navigation= this.router.getCurrentNavigation();
    this.error = navigation?.extras?.state?.error;
    
   }

  ngOnInit(): void {
    // console.log("Error Details "+ this.error.error.detail);
    // console.log("Error Message "+this.error.message);
    console.log("This is Server Message:- "+this.error.error.details);
  }

}
