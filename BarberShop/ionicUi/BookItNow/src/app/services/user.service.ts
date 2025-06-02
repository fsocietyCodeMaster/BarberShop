import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  BASE_URL = 'https://localhost:7148/api/';

  //'https://localhost:7148/api/BarberShop/barbershop' 

  constructor(private http: HttpClient,
    private router: Router) {
  }


  public register(userRegisterData: any) {
    return this.http.post(this.BASE_URL + 'Auth/register', userRegisterData)
  }

  public login(userRegisterData: any) {
    return this.http.post(this.BASE_URL + 'Auth/login', userRegisterData)
  }

  public create_barbershop(barbershopData: any) {
    const headers = new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('token')}`);
    return this.http.post(this.BASE_URL + 'BarberShop/barbershopform', barbershopData, { headers: headers })
  }

  public barbershop_list() {
    const headers = new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('token')}`);
    return this.http.get(this.BASE_URL + 'BarberShop/barbershoplists', { headers: headers })
  }

  public sendRequest(barbershopId: any) {
    const headers = new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('token')}`);
    return this.http.post(this.BASE_URL + `Barber/selectbarbershop?id=${barbershopId}`, '',{ headers: headers })
  }

  public barber_list(id: any) {
    const headers = new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('token')}`);
    return this.http.get(this.BASE_URL + 'BarberShop/barbershop?id='+ id , { headers: headers })
  }

  public getUserInfo() {
    const headers = new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('token')}`);
    return this.http.get(this.BASE_URL + 'User/userinfo', { headers: headers })

  }

  public Getbarberbybarbershop() {
    const headers = new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('token')}`);
    return this.http.get(this.BASE_URL + 'BarberShop/Getbarberbybarbershop', { headers: headers })

  }

  //BarberShop/Getbarberbybarbershop

}

