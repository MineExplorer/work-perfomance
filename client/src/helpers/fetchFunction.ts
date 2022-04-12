import { API_URL, AUTH_URL } from '../env.variables';

class HttpError extends Error {
  httpStatus: number;

  constructor(message: string, httpStatus: number) {
    super(message);
    this.httpStatus = httpStatus;
  }
}

const handleResponse = async <T>(response: Response): Promise<T> => {
  console.log(`Request ${response.url} - ${response.status}`)
  if (response.ok) {
    const contentType = response.headers.get('content-type');
    if (contentType && contentType.includes('application/json')) {
      const resp = await response.json();
      return resp || [];
    }
    return (response as unknown) as T;
  }
  throw new HttpError(`Response: [${response.status}] ${response.statusText}`, response.status);
};

export const fetchFunctionAuth = async <T>(urlParams: string): Promise<T> => {
  const res = await fetch(`${AUTH_URL}${urlParams}`);
  return handleResponse<T>(res);
};

export const postFunctionAuth = async <T>(urlParams: string, body: any): Promise<T> => {
  const res = await fetch(`${AUTH_URL}${urlParams}`, {
    method: 'POST',
    body,
    headers: {
      'Content-Type': 'application/json',
    },
  });
  return handleResponse<T>(res);
};

export const fetchFunctionApi = async <T>(urlParams: string | URL, requestInit?: RequestInit): Promise<T> => {
  let res;
  if (urlParams instanceof URL) {
    res = await fetch(urlParams.toString(), requestInit);
  } else {
    res = await fetch(`${API_URL}${urlParams}`, requestInit);
  }

  return handleResponse<T>(res);
};

export const postFunctionApi = async <T>(urlParams: string, body: any): Promise<T> => {
  const res = await fetch(`${API_URL}${urlParams}`, {
    method: 'POST',
    body,
    headers: {
      'Content-Type': 'application/json',
    },
  });
  return handleResponse<T>(res);
};

export const patchFunctionApi = async <T>(urlParams: string | URL, body: any): Promise<T> => {
  const res = await fetch(`${API_URL}${urlParams}`, {
    method: 'PATCH',
    body,
    headers: {
      'Content-Type': 'application/json',
    },
  });
  return handleResponse<T>(res);
};

export const putFunctionApi = async <T>(urlParams: string | URL, body: any): Promise<T> => {
  const res = await fetch(`${API_URL}${urlParams}`, {
    method: 'PUT',
    body,
    headers: {
      'Content-Type': 'application/json',
    },
  });
  return handleResponse<T>(res);
};

export const deleteFunctionApi = async <T>(urlParams: string | URL): Promise<T> => {
  const res = await fetch(`${API_URL}${urlParams}`, {
    method: 'DELETE',
  });
  return handleResponse<T>(res);
};
