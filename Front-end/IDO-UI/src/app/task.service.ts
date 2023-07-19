import { Injectable } from '@angular/core';
import { HttpClient , HttpErrorResponse, HttpHeaders, HttpResponse} from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class TaskService {

  constructor(private _http: HttpClient){}

  login(data : any){
    return this._http.post<HttpResponse<any>>(environment.LOGIN_URL , data);
  }

  getUserTasks(token : any){
    const headers = new HttpHeaders().set('Token', token);
    return this._http.get(environment.GET_TASK_URL , {headers});
  }

  editTask(data : any , token :any){
    const headers = new HttpHeaders().set('Token', token);
    return this._http.post(environment.EDIT_TASK_URL , data , {headers});
  }

  createTask(data : any , token : any){
    const headers = new HttpHeaders().set('Token', token);
    return this._http.post(environment.CREATE_TASK_URL , data , {headers});
  }
}
