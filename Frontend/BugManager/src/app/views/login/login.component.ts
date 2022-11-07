import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router } from '@angular/router';
import { FormBuilder, FormControl, Validators, FormGroup } from '@angular/forms';
import { LoginService } from 'src/app/services/login.service';
import { LoginResponse } from 'src/app/views/login/models/loginResponse';
import { InfoMessage } from 'src/app/components/message/model/message';
import { UserCredentials } from './models/userCredentials';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  constructor(private router: Router, private loginService: LoginService) { }


  form = new FormGroup({
    username: new FormControl('', [Validators.required]),
    password: new FormControl('', [Validators.required]),
  });

  credentials: UserCredentials = { username: '', password: '' }
  hide = true;
  loading = false;
  errorMessage: InfoMessage = { text: "", error: true };

  ngOnInit() {
    localStorage.removeItem("role");
    localStorage.removeItem("token");
  }


  LogIn() {
    this.loading = true;
    this.loginService.login(this.credentials).subscribe(

      (loginResponse: LoginResponse) => {
        localStorage.setItem("role", loginResponse.role);
        localStorage.setItem("token", loginResponse.token);
        this.router.navigateByUrl(`/${loginResponse.role}`);
        this.credentials = { username: '', password: '' };
      },

      error => {
        this.errorMessage.text = error
        this.loading = false;
      }
    );

  }
}
