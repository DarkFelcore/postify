import { HttpInterceptorFn } from '@angular/common/http';
import { SpinnerService } from '../services/spinner.service';
import { inject } from '@angular/core';
import { finalize } from 'rxjs';

export const loadingInterceptor: HttpInterceptorFn = (req, next) => {
  const spinnerService: SpinnerService = inject(SpinnerService);

  if (req.method === 'POST' && req.url.includes('like')) {
    return next(req);
  }

  spinnerService.busy();

  return next(req).pipe(finalize(() => spinnerService.idle()));
};
