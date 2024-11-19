import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { FileUploadModel } from 'src/models/fileUpload';

@Injectable({
  providedIn: 'root',
})
export class HttpService {
  private readonly apiUrl = 'https://localhost:7064/api/File/upload';

  constructor(private http: HttpClient) {}

  uploadFile(fileModel: FileUploadModel): Observable<any> {
    const formData = new FormData();
  formData.append('file', fileModel.file);
  formData.append('fileName', fileModel.fileName || '');
  formData.append('fileDescription', fileModel.fileDescription || '');
  formData.append('fileCategory', fileModel.fileCategory || '')
    return this.http.post(this.apiUrl, formData, {observe:'response', responseType:'text'});
  }
}
