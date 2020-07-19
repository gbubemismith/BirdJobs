import { TestBed } from '@angular/core/testing';

import { TwitterAuthService } from './twitter-auth.service';

describe('TwitterAuthService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: TwitterAuthService = TestBed.get(TwitterAuthService);
    expect(service).toBeTruthy();
  });
});
