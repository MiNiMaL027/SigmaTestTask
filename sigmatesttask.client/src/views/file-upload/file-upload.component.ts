import { Component } from '@angular/core';
import { FileUploadModel } from 'src/models/fileUpload';
import { HttpService } from 'src/services/httpClientService';

@Component({
  selector: 'file-upload',
  templateUrl: './file-upload.component.html',
  styleUrls: ['./file-upload.component.css'],
})
export class FileUploadComponent {
  selectedFile: File | null = null;
  fileDescription: string = '';
  fileCategory: string = '';
  fileName: string = '';
  uploadStatus: string = '';

  constructor(private httpService: HttpService) {}

  onFileSelected(event: Event): void {
    const input = event.target as HTMLInputElement;
    if (input?.files?.length) {
      this.selectedFile = input.files[0];
    }
  }

  uploadFile(): void {
    if (!this.selectedFile || !this.fileCategory) {
      this.uploadStatus = 'Please fill out all required fields!';
      return;
    }

    const reader = new FileReader();
    reader.onload = () => {
      const fileBytes = new Uint8Array(reader.result as ArrayBuffer);
      const fileModel: FileUploadModel = {
        file:this.selectedFile!,
        fileName: this.fileName,
        fileDescription: this.fileDescription,
        fileCategory: this.fileCategory,
      };

      this.httpService.uploadFile(fileModel).subscribe({
        next: () => (this.uploadStatus = 'File uploaded successfully!'),
        error: (err) => (this.uploadStatus = `Error: ${err.message}`),
      });
    };

    reader.onerror = () => {
      this.uploadStatus = 'Error reading file!';
    };

    reader.readAsArrayBuffer(this.selectedFile);
  }
}
