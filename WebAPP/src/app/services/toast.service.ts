import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class ToastService {
  toasts: { message: string; type: 'success' | 'error' | 'warning' }[] = [];

  /**
   * Add a toast notification
   * @param message The toast message
   * @param type The toast type ('success' | 'error' | 'warning')
   */
  addToast(message: string, type: 'success' | 'error' | 'warning'): void {
    this.toasts.push({ message, type });

    // Automatically remove the toast after 5 seconds
    setTimeout(() => {
      this.removeToast();
    }, 5000);
  }

  /**
   * Remove the oldest toast notification
   */
  removeToast(): void {
    this.toasts.shift();
  }
}
