import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { MycategoryParams } from '../shared/models/mycategoryParams';
import { IPagination4 } from '../shared/models/pagination4';

@Injectable({
  providedIn: 'root'
})
export class UsersService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getUsers(mycategoryParams: MycategoryParams) {
    let params = new HttpParams();
    if (mycategoryParams.query) {
      params = params.append('query', mycategoryParams.query);
    }
    params = params.append('page', mycategoryParams.pageNumber.toString());
    params = params.append('pageCount', mycategoryParams.pageSize.toString());
    return this.http.get<IPagination4>(this.baseUrl + 'users', {observe: 'response', params})
    .pipe(
      map(response  => {
        return response.body;
      })
    );
  }

unLockUser(userId: string) {
    return this.http.put(`${this.baseUrl}users/luckily/${userId}`, {});
}

lockUser(userId: string) {
    return this.http.put(`${this.baseUrl}users/${userId}`, {});
}

}
