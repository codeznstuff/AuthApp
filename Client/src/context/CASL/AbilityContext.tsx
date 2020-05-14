import React, { createContext, useContext } from 'react';
import { createContextualCan } from '@casl/react';
import { AbilityBuilder } from '@casl/ability';
import { useAuthorization } from '../Authorization/AuthorizationContext';
import { IClaimData, IApplicationUser } from '../../services/AuthorizationApi';

// Create Ability Context
export const AbilityContext = createContext(null);

// Create Can context that uses Ability Context
export const Can = createContextualCan(AbilityContext.Consumer);

// Creates the Ability Context Provider
export const AbilityProvider = (props: any): any => {
  const user = useAuthorization();
  const ability = defineRules(user);
  return <AbilityContext.Provider value={ability} {...props} />;
};

// Returns the type for the object passed into Can
const subjectName = (subject: any) => {
  if (!subject || typeof subject === 'string') {
    return subject;
  }
  if (subject.hasOwnProperty('userId')) {
    return 'user';
  }
  return typeof subject;
};

// Establishes the Permissions for the application
const defineRules = (user: IApplicationUser) => {
  return AbilityBuilder.define({ subjectName }, (can: any) => {
    // FOR DEMO ONLY!!!
    let adminClaim: any;
    var min = 0;
    var max = 1;
    var random = Math.floor(Math.random() * (max - min + 1) + min);
    if (random === 1) {
      adminClaim = user.claims ? user.claims.filter((claim: IClaimData) => claim.claimType === 'Role' && claim.claimValue === 'Admin') : [];
    } else {
      adminClaim = [];
    }

    // Admin roles gives full site access
    // const adminClaim = user.claims ? user.claims.filter((claim: IClaimData) => claim.claimType === 'Role' && claim.claimValue === 'Admin') : [];
    if (adminClaim.length > 0) {
      can(['read', 'create', 'edit', 'delete'], 'user');
      can('manage', 'all');
    }
    // Standard user permissions
    else {
      can(['edit', 'delete'], 'user', { userId: user.userId });
      can(['read', 'create'], 'user');
    }
  });
};

// Context Hook
export const useAbility = (): any => {
  const context: any = useContext(AbilityContext);
  if (context === undefined) {
    throw new Error(`useAbility must be used within an AbilityProvider`);
  }
  return context;
};
