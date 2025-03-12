// import { Injectable } from '@angular/core';
// import { CanActivate, Router } from '@angular/router';
// import { AuthService } from '@abp/ng.core';

// @Injectable({
//   providedIn: 'root',
// })
// export class AdminRoleGuard implements CanActivate {
//   constructor(private authService: AuthService, private router: Router) {}

//   canActivate(): boolean {
//     // Get user roles from AuthService
//     const policies = this.authService.getGrantedPolicies();
//     console.log('User Policies:', policies);

//     // Example: Allow only if the user has "Admin" policy
//     if (policies['Admin']) {
//       return true;
//     }

//     // Redirect if not authorized
//     this.router.navigate(['/']);
//     return false;
//   }
// }
