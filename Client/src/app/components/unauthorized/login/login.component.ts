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
import {MatProgressSpinner} from '@angular/material/progress-spinner';
import {Patterns} from '../../../constants/routes';

type email = string;

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
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class LoginComponent {

  authApi = inject(GeneralApiService);
  isLoginMode = signal<boolean>(true);
  authForm = new FormGroup({
    email: new FormControl<email>('', [Validators.required, Validators.email]),
    password: new FormControl<string>('', [Validators.required, Validators.pattern(Patterns.Password)]),
    confirmPassword: new FormControl('')
  });
  errorMessage = signal<string>('');
  isLoading = signal<boolean>(false);

  switchMode() {
    this.isLoginMode.update(login => !login);
    if (!this.isLoginMode()) {
      this.confirmPasswordControl?.setValidators([Validators.required, this.matchValues('password')]);
    } else {
      this.confirmPasswordControl?.clearValidators();
    }
    this.confirmPasswordControl?.updateValueAndValidity();
  }

  get confirmPasswordControl(): FormControl {
    return this.authForm.controls?.confirmPassword;
  }

  get email(): string {
    return this.authForm.controls?.email?.value;
  }

  get password(): string {
    return this.authForm?.controls?.password?.value;
  }

  onSubmit() {
    this.isLoading.set(true);

    if (!this.isLoginMode() && this.password !== this.confirmPasswordControl.value) {
      this.errorMessage.set('Passwords do not match');
      this.isLoading.set(false);
      return;
    }

    const generalErrorMessage = 'user name or password incorrect';

    this.authApi.loginOrRegister(this.email, this.password, this.isLoginMode())
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
        ? null : {isMatching: false};
    };
  }
}
