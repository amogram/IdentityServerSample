import {Injectable} from '@angular/core';
import {Http, Headers} from '@angular/http';

@Injectable()
export class WebServiceClient {
    constructor(private http: Http) { }

    // Creates an Authorization Header
    createAuthHeader(headers: Headers) {
        headers.append('Authorization', 'Basic');
    }

    // Creates an Api Version header
    createVersionHeader(headers: Headers, version: string) {
        headers.append('Api-Version', version);
    }

    // Performs a GET with version
    get(url, version) {
        let headers = new Headers();
        this.createVersionHeader(headers, version);
        return this.http.get(url,
            {
                headers: headers
            });
    }

    // Performs a POST with version
    post(url, data, version) {
        let headers = new Headers();
        this.createVersionHeader(headers, version);
        return this.http.post(url,
            data,
            {
                headers: headers
            });
    }
}