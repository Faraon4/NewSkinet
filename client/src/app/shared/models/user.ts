export interface User {
    // make sure to have the correct spelling
    email: string;
    displayName: string;
    token: string;
}

export interface Address {
    firstName: string;
    lastName: string;
    street: string;
    city: string;
    state: string;
    zipcode: string;
}