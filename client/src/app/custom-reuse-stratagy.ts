import { ActivatedRouteSnapshot, BaseRouteReuseStrategy, DetachedRouteHandle } from "@angular/router";

export class CustomReuseStrategy implements BaseRouteReuseStrategy {
    shouldDetach(route: ActivatedRouteSnapshot): boolean {
      return false; // Implement logic if routes should be detached
    }
  
    store(route: ActivatedRouteSnapshot, handle: DetachedRouteHandle | null): void {
      // Implement storing of routes if shouldDetach returns true
    }
  
    shouldAttach(route: ActivatedRouteSnapshot): boolean {
      return false; // Implement logic if routes should be reattached
    }
  
    retrieve(route: ActivatedRouteSnapshot): DetachedRouteHandle | null {
      return null; // Implement retrieval of stored routes if shouldAttach returns true
    }
  
    shouldReuseRoute(future: ActivatedRouteSnapshot, curr: ActivatedRouteSnapshot): boolean {
      return false; // Implement logic for route reuse
    }
  }