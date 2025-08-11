import { HttpClient } from '@angular/common/http';
import { Component, inject } from '@angular/core';
import { AppUser } from '../../models/app-user.model';
import { MatButtonModule } from '@angular/material/button';
import { FormBuilder, FormControl, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';

@Component({
  selector: 'app-member',
  imports: [
    MatButtonModule, MatFormFieldModule, MatInputModule,
    FormsModule, ReactiveFormsModule
  ],
  templateUrl: './member.component.html',
  styleUrl: './member.component.scss'
})
export class MemberComponent {
  private _http = inject(HttpClient);
  private _fB = inject(FormBuilder);

  users: AppUser[] | undefined;
  userRes: AppUser | undefined;
  error: string | undefined;

  //#region fG
  userFg = this._fB.group({
    usernameCtrl:['']
  })

    get UserNameCtrl(): FormControl {
    return this.userFg.get('usernameCtrl') as FormControl;
  }
  //#endregion

  getAll(): void {
    this._http.get<AppUser[]>('http://localhost:5000/api/user/get-all').subscribe({
      next: (res) => {
        console.log(res);
        this.users = res;
      }
    })
  }

  getByUserName(): void {
    let req = this.UserNameCtrl.value

    this._http.get<AppUser>('http://localhost:5000/api/user/get-by-username/' + req).subscribe({
      next: (res) => {
        console.log(res);
        this.userRes = res;
      },
      error: (err) => this.error = err.err
    })
  }
}
