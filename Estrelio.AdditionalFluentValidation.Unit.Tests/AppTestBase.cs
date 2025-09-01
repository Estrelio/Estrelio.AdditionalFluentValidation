// -----------------------------------------------------------------------
// <copyright file="AppTestBase.cs" company="Estrelio">
// Copyright (c) Estrelio. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Estrelio.AdditionalFluentValidation.Unit.Tests;

public class AppTestBase<TAppFixture>(TAppFixture app) : IClassFixture<TAppFixture>
    where TAppFixture : App
{
    public Faker Faker => this.App.Faker;

    protected TAppFixture App => app;
}