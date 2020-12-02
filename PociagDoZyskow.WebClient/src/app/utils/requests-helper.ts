import { Injectable } from '@angular/core';
import { HttpClient,  HttpHeaders } from '@angular/common/http';
import { Observable } from "rxjs"
import 'rxjs/add/operator/catch'
import 'rxjs/add/operator/shareReplay'

import { environment } from '../../environments/environment';
import { JwtForm } from '../viewmodels/account/JwtForm';

@Injectable({
    providedIn: 'root',
})
export class RequestsHelper {
    constructor(private http: HttpClient) { }

    // get<T>(url): Observable<T> {
    //     let request = this.http.get<T>(environment.apiUrl + url);
    //     return request;
    // }

    get<T>(url): Observable<any> {
        let token = localStorage.getItem('jwt');
        if (token != null && token != undefined) 
        {
            let jwt = new JwtForm(token);
            const httpOptions = {
                headers: new HttpHeaders({
                  'Content-Type':  'application/json',
                  'Authorization': `Bearer ${jwt.auth_token}`
                })
            };
            return this.http.get<T>(environment.apiUrl + url, httpOptions);
        } else {
            return this.http.get<T>(environment.apiUrl + url);
        }
    }

    post<T>(url, postObject): Observable<T> {
        let token = localStorage.getItem('jwt');
        if (token != null && token != undefined) {
            let jwt = new JwtForm(token);
            const httpOptions = {
                headers: new HttpHeaders({
                  'Content-Type':  'application/json',
                  'Authorization': `Bearer ${jwt.auth_token}`
                })
            };
            return this.http.post<T>(environment.apiUrl + url, postObject, httpOptions);
        } else {
            return this.http.post<T>(environment.apiUrl + url, postObject);
        }
    }

    
    post_simple(url, postObject): Observable<any> {
        let token = localStorage.getItem('jwt');
        if (token != null && token != undefined) {
            let jwt = new JwtForm(token);
            const httpOptions = {
                headers: new HttpHeaders({
                  'Content-Type':  'application/json',
                  'Authorization': `Bearer ${jwt.auth_token}`
                })
            };
            return this.http.post(environment.apiUrl + url, postObject, httpOptions);
        } else {
            return this.http.post(environment.apiUrl + url, postObject);
        }
    }


    // getWithParam<T>(url, param): Observable<any> {
    //     let token = localStorage.getItem('jwt');
    //     if (token != null && token != undefined) 
    //     {
    //         let jwt = new JwtForm(token);
    //         const httpOptions = {
    //             headers: new HttpHeaders({
    //               'Content-Type':  'application/json',
    //               'Authorization': `Bearer ${jwt.auth_token}`
    //             })
    //         };
    //         return this.http.get<T>(environment.apiUrl + url, httpOptions);
    //     } else {
    //         return this.http.get<T>(environment.apiUrl + url);
    //     }
    // }

    // getWithForm<T>(url, param): Observable<any> {
    //     return this.http.get<T>(environment.apiUrl + url, param);
    // }

    // getWithUrlParam<T>(url, params): Observable<any> {
    //     return this.http.get<T>(environment.apiUrl + url, params);
    // }

    // postWithToken<T>(url, postObject): Observable<T> {
    //     let token = localStorage.getItem('jwt');
    //     if (token != null && token != undefined) {
    //         let jwt = new JwtForm(token);
    //         const httpOptions = {
    //             headers: new HttpHeaders({
    //               'Content-Type':  'application/json',
    //               'Authorization': `Bearer ${jwt.auth_token}`
    //             })
    //         };
    //         return this.http.post<T>(environment.apiUrl + url, postObject, httpOptions);
    //     } else {
    //         return this.http.post<T>(environment.apiUrl + url, postObject);
    //     }
    // }

}