import axios, { AxiosInstance, AxiosRequestConfig, AxiosResponse } from 'axios'
import { authHeader } from './AuthHeader'

export class HttpRequest<T> {
  private api: AxiosInstance

  public constructor(config: AxiosRequestConfig) {
    const authHeaderConfig = authHeader()
    const customConfig = {
      headers: {
        common: {
          ...authHeaderConfig,
          'Cache-Control': 'no-cache, no-store, must-revalidate',
          Pragma: 'no-cache',
          'Content-Type': 'application/json',
          Accept: 'application/json',
        },
      },
    }

    this.api = axios.create({ ...customConfig, ...config })

    // this middleware is been called right before the http request is made.
    this.api.interceptors.request.use((param: AxiosRequestConfig) => ({
      ...param,
    }))

    // this middleware is been called right before the response is get it by the method that triggers the request
    this.api.interceptors.response.use((param: AxiosResponse) => ({
      ...param,
    }))
  }

  public get<T, R = AxiosResponse<T>>(
    url: string,
    config?: AxiosRequestConfig
  ): Promise<R> {
    return this.api.get(url, config)
  }

  public delete<T, R = AxiosResponse<T>>(
    url: string,
    config?: AxiosRequestConfig
  ): Promise<R> {
    return this.api.delete(url, config)
  }

  public post<T, R = AxiosResponse<T>>(
    url: string,
    data?: any,
    config?: AxiosRequestConfig
  ): Promise<R> {
    return this.api.post(url, data, config)
  }

  public put<T, R = AxiosResponse<T>>(
    url: string,
    data?: any,
    config?: AxiosRequestConfig
  ): Promise<R> {
    return this.api.put(url, data, config)
  }

  public patch<T, R = AxiosResponse<T>>(
    url: string,
    data?: any,
    config?: AxiosRequestConfig
  ): Promise<R> {
    return this.api.patch(url, data, config)
  }
}
