import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SharedService {
readonly APIURL="http://localhost:1068/api";
readonly PhotoURL="http://localhost:1068/Photos/";
  constructor(private http:HttpClient) { }

getDepList():Observable<any[]>{
  return this.http.get<any>(this.APIURL+'/Department');
}

addDepartment(val:any){
  return this.http.post(this.APIURL+'/Department',val);
}

updateDepartment(val:any){
  return this.http.put(this.APIURL+'/Department',val);
}

deleteDepartment(val:any){
  return this.http.delete(this.APIURL+'/Department/'+val);
}

getEmpList():Observable<any[]>{
  return this.http.get<any>(this.APIURL+'/Employee');
}

addEmployee(val:any){
  return this.http.post(this.APIURL+'/Employee',val);
}

updateEmployee(val:any){
  return this.http.put(this.APIURL+'/Employee',val);
}

deleteEmployee(val:any){
  return this.http.delete(this.APIURL+'/Employee/'+val);
}

UploadPhoto(val:any){
  return this.http.post(this.APIURL+'/Employee/SaveFile',val);
}

getAllDepartmentName():Observable<any[]>{
  return this.http.get<any>(this.APIURL+'/Employee/GetAllDepartmentName');
}



  
}
