import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.css']
})
export class LoginComponent {

    constructor(private router: Router) { }
    username: string;
    password: string;
    error: string = '';

    login(): void {
        if (this.username == 'teste@swfast.com.br' && this.password == '1234') {
            localStorage.setItem('logged','true');
            this.router.navigate(["index"]);
        } else {
            this.error = 'Usuario e/ou senha estão incorretos.'
        }
    }
}
