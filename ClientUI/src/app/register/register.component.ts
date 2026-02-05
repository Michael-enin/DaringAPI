import { Component, Input, OnInit, Output, EventEmitter} from '@angular/core';
import { AbstractControl, 
         FormBuilder, 
         FormControl, 
         FormGroup, 
         NgControl, 
         ValidatorFn, 
         Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

import { AccountService } from '../_services/account.service';
@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  //intput values from parent component child
  @Input() usersFromHomeComponent :any
  // output values from child to parent
  @Output() cancelRegister = new EventEmitter<boolean>();
  model:any={};//this property needs to be changed to registerForm
  registerForm!:FormGroup;
  fine!:string[];
  ngControl!:NgControl;
  validationErrors:string[]=[];
  constructor(private accountService: AccountService, 
            private toastr : ToastrService, 
            private formBuilder:FormBuilder, 
            private router:Router){ }
  ngOnInit(): void {
   this.initializeForm();
   this.fine=["hey", "lol"]
  }
  register(){
    //"this.model" needs to be changed to our form "registerForm"
    //this.accountService.register(this.model).subscribe
            this.accountService.register(this.registerForm.value).subscribe(
              responseData=>
                      {
                            console.log(responseData);
                            this.router.navigateByUrl('/members');
                      },
                      
              error=>{
                        console.log(error);
                      this.validationErrors=error;
                        
                      }, 
                      ()=>{
              console.log("Registration completed!");
              this.cancelRegistration();  
            })
                  console.log(this.model);
     //   console.log(this.registerForm.value);
        //the moment you register you need to navigate to memebers page;
      
        
            
  }
  cancelRegistration(){
    // console.log("cancelled");
    // this.model={}
    this.cancelRegister.emit(false);
  }
  initializeForm(){
      this.registerForm = this.formBuilder.group({
              gender:['male'],
              username:['', Validators.required], 
              knownAs:['', Validators.required],
              birthDate:['', Validators.required],
              city:['', Validators.required], 
              country:['', Validators.required],
              password:['', [Validators.required, 
                                            Validators.minLength(4), 
                                            Validators.maxLength(8)]], 
              comfirmPassword:['', [Validators.required, 
                               this.matchPasswords("password")]]
      })
  }
  matchPasswords(pass: string): ValidatorFn {
    return (control: AbstractControl) =>{
     // return control?.value === control?.parent?.controls[""].value ? null : {isMatch:true};
    //  control.parent?.controls[""]
    const controls = control?.parent?.controls as {[key:string]:AbstractControl;};
    let MatchControl=null;
    if(controls) MatchControl = controls[pass];
    return control?.value === MatchControl?.value ?  null: {isMatching:true}
    }
  }
  get control(){
   return this.ngControl.control as FormControl
  }

}
