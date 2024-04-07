import {ChangeDetectionStrategy, Component, inject, model, signal} from '@angular/core';
import {AsyncPipe, CurrencyPipe, NgOptimizedImage} from '@angular/common';
import {FormControl, FormGroup, FormsModule, ReactiveFormsModule} from '@angular/forms';
import {MatFormField, MatLabel} from '@angular/material/form-field';
import {MatInput} from '@angular/material/input';
import {BehaviorSubject, debounceTime, filter, finalize, switchMap, tap} from 'rxjs';
import {GeneralApiService} from '../../services/general-api.service';
import {MatAutocomplete, MatAutocompleteTrigger, MatOption} from '@angular/material/autocomplete';
import {MatProgressSpinner} from '@angular/material/progress-spinner';
import {MatCard} from '@angular/material/card';
import {GitHubRepo} from '../../models/github-model';
import {MatIcon} from '@angular/material/icon';

@Component({
  selector: 'app-travel-cover',
  standalone: true,
  imports: [
    CurrencyPipe,
    FormsModule,
    MatFormField,
    MatInput,
    MatLabel,
    ReactiveFormsModule,
    AsyncPipe,
    NgOptimizedImage,
    MatAutocomplete,
    MatOption,
    MatAutocompleteTrigger,
    MatProgressSpinner,
    MatCard,
    MatIcon
  ],
  templateUrl: './gallery.component.html',
  styleUrl: './gallery.component.css',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class GalleryComponent {

  searchApi = inject(GeneralApiService);
  refresh$ = new BehaviorSubject<string>('');
  repos$ = this.refresh$
    .pipe(
      filter(search => !!search),
      tap(() => this.isLoading.set(true)), // Start loading
      debounceTime(300),
      switchMap(search =>
        this.searchApi.searchRepos(search)
      ),
      tap(() => this.isLoading.set(false))
    );
  searchForm = new FormGroup({
      search: new FormControl('')
    });

  isLoading = signal<boolean>(false);

  onInput() {
    this.refresh$.next(this.searchTerm);
  }

  get searchTerm(): string {
    return this.searchForm.get('search')?.value;
  }

  addBookmark(repo: GitHubRepo) {
    return this.searchApi.addBookmark(repo);
  }
}
