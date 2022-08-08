This repository holds the sample code for my blog post about the Polly library.

[https://duongnt.com/polly-retry-request](https://duongnt.com/polly-retry-request)

# Usage

## Run the tests from Visual Studio Test Explorer

- Open the solution in Visual Studio.
- From the Test Explorer window, right click `MockIHttpClientFactoryDemoTests` and select `Run`.
- You should see all tests pass.

![Run tests via Visual Studio](/image/mock-ihttpclientfactory-test-vs.PNG)

## Run the tests from the command line

- From the command line, enter the solution's folder.
- Execute the following command.
    ```
    dotnet test
    ```
- You should see the following result.
    ```
    Starting test execution, please wait...
    A total of 1 test files matched the specified pattern.

    Passed!  - Failed:     0, Passed:     2, Skipped:     0, Total:     2, Duration: 10 ms - MockIHttpClientFactoryDemoTests.dll (netcoreapp3.1)
    ```

# License

https://opensource.org/licenses/MIT
