import { Component, OnInit } from '@angular/core';
import {  FormBuilder, FormGroup, Validators } from '@angular/forms';
import { TokenService } from '../token.service';
import { TaskService } from '../task.service';
import { Router } from '@angular/router';
import { HttpResponse } from '@angular/common/http';
import { catchError, of, tap } from 'rxjs';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup;
  invalidCredentials : boolean = false;

  constructor(private formBuilder: FormBuilder , private tokenService : TokenService , private service : TaskService , private router : Router) { 
    this.loginForm = this.formBuilder.group({
      Email: ['', Validators.required],
      Password: ['', Validators.required]
    });
  }
  
  ngOnInit(): void {
  }
  onSubmit(){
    this.service.login(this.loginForm.value).pipe(
      tap((result: any) => {
        if(result.token){
          this.tokenService.setToken(result.token);
          this.router.navigate(["/to-do-list"]);
        }
      }),
      catchError((error: any) => {
        this.loginFailed();
        return of(error);
      })
    ).subscribe()
  }

  loginFailed(){
    this.invalidCredentials = true;
    setTimeout(() => {
      this.invalidCredentials = false;
    }, 2000);
  }
}
