import { Routes } from '@angular/router';
import { RegisterComponent } from './components/register/register.component';
import { UpdateComponent } from './components/update/update.component';
import { DeleteComponent } from './components/delete/delete.component';
import { MemberComponent } from './components/member/member.component';

export const routes: Routes = [
    { path: 'register', component: RegisterComponent },
    { path: 'member', component: MemberComponent },
    { path: 'update', component: UpdateComponent },
    { path: 'delete', component: DeleteComponent }
];
