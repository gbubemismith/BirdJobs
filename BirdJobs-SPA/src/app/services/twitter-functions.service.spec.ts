import { TestBed } from '@angular/core/testing';

import { TwitterFunctionsService } from './twitter-functions.service';

describe('TwitterFunctionsService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: TwitterFunctionsService = TestBed.get(TwitterFunctionsService);
    expect(service).toBeTruthy();
  });
});
