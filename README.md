# Optimizely-Test

This app will check the effective rollout percentage for a given set of user attributes.

## How to build

```bash
dotnet build
```

## Usage

### From source code

```bash
./dotnet run -- -h
./dotnet run --
    --sdk-key [SDK key]
    --feature [name of feature]
    --iterations [number of iterations]
    --userId [value that uniquely identifies the user]
    --attributes [colon-delimited, space separated user attributes]
```

### Built app

```bash
./optimizely-test -h
./optimizely-test
    --sdk-key [SDK key]
    --feature [name of feature]
    --iterations [number of iterations]
    --attributes [colon-delimited, space separated user attributes]
```

## Example

```bash
./optimizely-test --sdk-key DUM4Asnd3C8G7Befbq5xb --feature my_cool_feature --iterations 1000 --attributes hostname:beta.mysite.com ip_address:84.64.145.96 device:ios
```