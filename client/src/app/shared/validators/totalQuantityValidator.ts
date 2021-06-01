import { AbstractControl, ValidatorFn } from '@angular/forms';

export function totalQuantityValidator(): ValidatorFn {

    return (control: AbstractControl) => {
        const value = control.value as number;
        if (!value) {
            return; }

        const quantity = value;
        if (quantity > this.totalQuantity) {
            return {
                totalQuantityValidator : {
                    message: 'You are selling to much!'
                }
            };
        }
        return;
    };
}
