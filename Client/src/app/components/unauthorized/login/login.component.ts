import {ChangeDetectionStrategy, Component, inject, signal} from '@angular/core';
import {MatTab, MatTabGroup} from '@angular/material/tabs';
import {MatError, MatFormField, MatLabel} from '@angular/material/form-field';
import {
  AbstractControl,
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  ValidationErrors,
  ValidatorFn,
  Validators
} from '@angular/forms';
import {MatInput} from '@angular/material/input';
import {MatButton} from '@angular/material/button';
import {GeneralApiService} from '../../../services/general-api.service';
import {catchError, finalize, tap} from 'rxjs';
import {RoutesNames} from '../../../constants/routes';
import {Router} from '@angular/router';
import {MatProgressSpinner} from '@angular/material/progress-spinner';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    MatTabGroup,
    MatTab,
    MatFormField,
    ReactiveFormsModule,
    MatInput,
    MatButton,
    MatLabel,
    MatError,
    MatProgressSpinner
  ],
  providers: [],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class LoginComponent {

  authApi = inject(GeneralApiService);
  isLoginMode = true;
  authForm = new FormGroup(
    {
      email: new FormControl('', [Validators.required, Validators.email]),
      password:   new FormControl('', [Validators.required, Validators.minLength(6)]),
      confirmPassword:  new FormControl( '')
    }
  );
  errorMessage = signal<string>('');
  isLoading = signal<boolean>(false);

  switchMode() {
    this.isLoginMode = !this.isLoginMode;
    if (!this.isLoginMode) {
      this.confirmPasswordControl?.setValidators([Validators.required, this.matchValues('password')]);
    } else {
      this.confirmPasswordControl?.clearValidators();
    }
    this.confirmPasswordControl?.updateValueAndValidity();
  }

  get confirmPasswordControl() {
    return this.authForm.get('confirmPassword');
  }

  get email(): string {
    return this.authForm.get('email')?.value;
  }

  get password(): string {
    return this.authForm.get('password')?.value;
  }

  onSubmit() {
    this.isLoading.set(true);
    const loginOrRegister = this.isLoginMode? (email, password) => this.authApi.login(email, password) :
      (email, password) => this.authApi.register(email, password);

    const generalErrorMessage = 'user name or password incorrect';

    if (!this.isLoginMode && this.password !== this.confirmPasswordControl.value) {
      this.errorMessage.set('Passwords do not match');
      return;
    }

    loginOrRegister(this.email, this.password )
        .pipe(
          tap((res) => this.errorMessage.set(res?.message ?? generalErrorMessage)),
          catchError(() => {
            this.errorMessage.set(generalErrorMessage);
            return [];
          }),
          finalize(() => this.isLoading.set(false))
        ).subscribe();
  }

  private matchValues(matchTo: string): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
      return !!control.parent &&
      !!control.parent.value &&
      control.value === control.parent.controls[matchTo].value
        ? null : { isMatching: false };
    };
  }
}
