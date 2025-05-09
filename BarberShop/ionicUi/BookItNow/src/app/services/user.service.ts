import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  BASE_URL = 'https://localhost:7148/api/';

  constructor(private http: HttpClient,
    private router: Router) {
  }


  public register(userRegisterData: any) {
    return this.http.post(this.BASE_URL + 'Auth/register', userRegisterData)
  }

  public login(userRegisterData: any) {
    return this.http.post(this.BASE_URL + 'Auth/login', userRegisterData)
  }


}

//password ---------------
//Puya@2000
