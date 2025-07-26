import { HttpClient } from '@angular/common/http';
import { Component, inject } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-delete',
  imports: [
    MatButtonModule
  ],
  templateUrl: './delete.component.html',
  styleUrl: './delete.component.scss'
})
export class DeleteComponent {
  private _http = inject(HttpClient);

  delete(): void {
    this._http.delete
      ('http://localhost:5000/api/user/delete-by-id/6874dd62da630f9bcf1a5c01').subscribe();
  }
}
